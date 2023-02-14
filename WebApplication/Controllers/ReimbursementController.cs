using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class ReimbursementController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly AppDbContext _context;
        public ReimbursementController(IConfiguration config, AppDbContext context)
        {
            _config = config;
            _context = context;
        }
        [HttpPost("AddReimbursement")]
        public IActionResult AddReimbursement(ClaimObj c)
        {
            try
            {
               
                Claims claim = new Claims();
                claim.UserId = Int32.Parse(c.id);
                claim.Date = c.Date;
                claim.Type = c.Type;
                claim.RValue = Int32.Parse(c.RValue);
                claim.Avalue = 0;
                claim.Phase = "to be processed";
                claim.CurrType = c.CurrType;
                claim.Link = c.Link;

                _context.ClaimsTable.Add(claim);
                _context.SaveChanges();
                return Ok("Added successfully.");
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }

        }
        [HttpGet("{id}")]
        public IActionResult GetReimbursementByUserId(string id)
        {
            int userid = Int32.Parse(id);
            var allReimbursement = _context.ClaimsTable.Where(t => t.UserId == userid).AsEnumerable<Claims>();
            return Ok(allReimbursement);
        }
        [HttpPut("EditReimbursement")]
        public IActionResult EditReimbursement(ClaimObj c)
        {
            try
            {
                var claim = _context.ClaimsTable.Where(t => t.id == Int32.Parse(c.id)).FirstOrDefault();
                claim.Date = c.Date;
                claim.Type = c.Type;
                claim.RValue = Int32.Parse(c.RValue);
                claim.Link = c.Link;
                claim.CurrType = c.CurrType;
                _context.ClaimsTable.Update(claim);
                _context.SaveChanges();
                return Ok("Updated successfully.");
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteReimbursement(string id)
        {
            try
            {
                var claim = _context.ClaimsTable.Where(t => t.id == Int32.Parse(id)).FirstOrDefault();
                _context.ClaimsTable.Remove(claim);
                _context.SaveChanges();
                return Ok("Removed");
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var allClaims = _context.ClaimsTable.ToList();
            List<AdminObj> obj = new List<AdminObj>();
            foreach (var i in allClaims)
            {
                var user = _context.UserTable.Where(t => t.id == i.UserId).FirstOrDefault();
                AdminObj o = new AdminObj();
                o.Date = i.Date;
                o.Id = i.id.ToString();
                o.Email = user.Email;
                o.CurrType = i.CurrType;
                o.RValue = i.RValue;
                o.Phase = i.Phase;
                o.Link = i.Link;
                o.Avalue = i.Avalue;
                o.Type = i.Type;
                obj.Add(o);
            }
            return Ok(obj);
        }

        [HttpPut("{id}")]
        public IActionResult DeclineReimbursement(string id)
        {
            try
            {
                var claim = _context.ClaimsTable.Where(t => t.id == Int32.Parse(id)).FirstOrDefault();
                claim.Phase = "Declined";
                _context.ClaimsTable.Update(claim);
                _context.SaveChanges();
                return Ok("Declined");
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
        }
        [HttpPut("ApproveReimbursement")]
        public IActionResult ApproveReimbursement(ApproveObj a)
        {
            try
            {
                var claim = _context.ClaimsTable.Where(t => t.id == Int32.Parse(a.id)).FirstOrDefault();
                claim.Phase = "Approved";
                claim.Avalue = Int32.Parse(a.Avalue);
                _context.ClaimsTable.Update(claim);
                _context.SaveChanges();
                return Ok("Approved");
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
        }


    }
}

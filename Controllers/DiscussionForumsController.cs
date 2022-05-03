﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Industry_4._0.Data;
using Industry_4._0.Models;

namespace Industry_4._0.Controllers
{
    public class DiscussionForumsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DiscussionForumsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DiscussionForums
        public async Task<IActionResult> Index()
        {
            return View(await _context.DiscussionForum.ToListAsync());
        }

        // GET: DiscussionForums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discussionForum = await _context.DiscussionForum
                .FirstOrDefaultAsync(m => m.Id == id);
            if (discussionForum == null)
            {
                return NotFound();
            }

            return View(discussionForum);
        }

        // GET: DiscussionForums/Create
        [Authorize(Roles = "Manager, RegisteredUser")]
        public IActionResult Create()
        {
            DiscussionForum df = new DiscussionForum();
            df.PostDate = DateTime.Now;
            df.UserName = User.Identity.Name;
            df.Like = 5;
            return View(df);
            //return View();
        }

        // POST: DiscussionForums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager, RegisteredUser")]
        public async Task<IActionResult> Create([Bind("Id,PostDate,UserName,TopicTitle,MessageContent,Heading,Agree,Disagree,Rating")] string page, string folder, DiscussionForum discussionForum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(discussionForum);
                await _context.SaveChangesAsync();
                return RedirectToAction(page, folder);
            }
            return View(discussionForum);
        }

        // GET: DiscussionForums/Edit/5
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discussionForum = await _context.DiscussionForum.FindAsync(id);
            if (discussionForum == null)
            {
                return NotFound();
            }
            return View(discussionForum);
        }

        // POST: DiscussionForums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Edit(int id, string page, string folder, [Bind("Id,PostDate,UserName,TopicTitle,MessageContent,Heading,Agree,Disagree,Rating")] DiscussionForum discussionForum)
        {
            if (id != discussionForum.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(discussionForum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiscussionForumExists(discussionForum.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(page, folder);
            }
            return View(discussionForum);
        }

        // GET: DiscussionForums/Delete/5
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discussionForum = await _context.DiscussionForum
                .FirstOrDefaultAsync(m => m.Id == id);
            if (discussionForum == null)
            {
                return NotFound();
            }

            return View(discussionForum);
        }

        // POST: DiscussionForums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> DeleteConfirmed(int id, string page, string folder)
        {
            var discussionForum = await _context.DiscussionForum.FindAsync(id);
            _context.DiscussionForum.Remove(discussionForum);
            await _context.SaveChangesAsync();
            return RedirectToAction(page, folder);
        }

        private bool DiscussionForumExists(int id)
        {
            return _context.DiscussionForum.Any(e => e.Id == id);
        }


        
        public async Task<IActionResult> IncreaseLike(int? id)
        {
            if (id == null) { return NotFound(); }
            var discussionForum = await _context.DiscussionForum.FindAsync(id); if (discussionForum == null) { return NotFound(); }
            if (ModelState.IsValid)
            {
                try { discussionForum.Like++; _context.Update(discussionForum); await _context.SaveChangesAsync(); }
                catch (DbUpdateConcurrencyException)
                { if (!DiscussionForumExists(discussionForum.Id)) { return NotFound(); } else { throw; } }
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> IncreaseAgree(int? id, string page, string folder)
        {
            if (id == null) { return NotFound(); }
            var discussionForum = await _context.DiscussionForum.FindAsync(id); if (discussionForum == null) { return NotFound(); }
            if (ModelState.IsValid)
            {
                try
                {
                   
                        discussionForum.Agree++;
                        discussionForum.canIncreaseAgree = false;
                        _context.Update(discussionForum);
                        await _context.SaveChangesAsync();
                    
                }
                catch (DbUpdateConcurrencyException)
                { if (!DiscussionForumExists(discussionForum.Id)) { return NotFound(); } else { throw; } }
                return RedirectToAction(page, folder);
            }
            return RedirectToAction(page, folder);
        }
        public async Task<IActionResult> IncreaseDisagree(int? id, string page, string folder)
        {
            if (id == null) { return NotFound(); }
            var discussionForum = await _context.DiscussionForum.FindAsync(id); if (discussionForum == null) { return NotFound(); }
            if (ModelState.IsValid)
            {
                try
                {
                    
                        discussionForum.Disagree++;
                        discussionForum.canIncreaseDisagree = false;
                        _context.Update(discussionForum);
                        await _context.SaveChangesAsync();
                    
                }
                catch (DbUpdateConcurrencyException)
                { if (!DiscussionForumExists(discussionForum.Id)) { return NotFound(); } else { throw; } }
                return RedirectToAction(page, folder);
            }
            return RedirectToAction(page, folder);
        }
    }

}
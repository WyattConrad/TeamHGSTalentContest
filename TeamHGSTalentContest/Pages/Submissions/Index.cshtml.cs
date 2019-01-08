﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TeamHGSTalentContest.Data;
using TeamHGSTalentContest.Models;
using TeamHGSTalentContest.ViewModels;

namespace TeamHGSTalentContest.Pages.Submissions
{
    public class IndexModel : PageModel
    {
        private readonly TeamHGSTalentContest.Data.ApplicationDbContext _context;

        public IndexModel(TeamHGSTalentContest.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<SubmissionViewModel> Submission { get;set; }

        public async Task OnGetAsync()
        {
            var submissions = await _context.Submissions.Include(e => e.Location).ToListAsync();
            var vm = submissions.Select(e => new SubmissionViewModel
            {
                FirstName = e.FirstName,
                LastName = e.LastName,
                PhoneNumber = e.PhoneNumber,
                Email = e.Email,
                ManagerName = e.ManagerName,
                DateCreated = e.DateCreated,
                Talent = e.Talent,
                FileName = e.FileName,
                Id = e.Id,
                LocationName = e.Location.Name
            }).OrderByDescending(e => e.DateCreated).ToList();
            Submission = vm;
        }
    }
}
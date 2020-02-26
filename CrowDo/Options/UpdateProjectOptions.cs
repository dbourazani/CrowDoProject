﻿using CrowDo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace CrowDo.Options
{
    public class UpdateProjectOptions
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public decimal Budget { get; set; }
        public string Description { get; set; }
        public Project User { get; set; }
        public ProjectCategory? Category { get; set; }
        public DateTime? CreationDate { get; set; }
        public StatusProject? StatusProject { get; set; }
    }
}

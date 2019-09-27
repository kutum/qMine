using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace qMine.Models
{
    public class MapView
    {
        /// <summary>
        /// Server is online
        /// </summary>
        [Display (Name ="Server is online")]
        public bool ServerUp { get; set; }
        /// <summary>
        /// Dynmap Url
        /// </summary>
        [Display (Name = "Dynmap Link")]
        public string MapUrl { get; set; }

    }
}
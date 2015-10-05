using System.Collections.Generic;
using UniversitySystem.Entities;

namespace UniversitySystem.ViewModel.Infos
{
    public class InfoInfoPageVM
    {
        /// <summary>
        /// Collection which will be set with article for visualization in the view.
        /// </summary>
        public ICollection<Info> Info { get; set; }

        /// <summary>
        /// String property for sorting the collection by keyword
        /// </summary>
        public string Search { get; set; }

        /// <summary>
        /// Property for category id associated with article
        /// </summary>
        public int CategoryID { get; set; }

        /// <summary>
        /// Property for category name associated with article
        /// </summary>
        public string CategoryName { get; set; }

        public string FilesPath { get; set; }

        public bool CanBeEdited { get; set; }
    }
}
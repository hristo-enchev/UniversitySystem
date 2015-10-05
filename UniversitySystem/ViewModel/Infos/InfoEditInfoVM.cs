using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UniversitySystem.Entities;

namespace UniversitySystem.ViewModel.Infos
{
    public class InfoEditInfoVM
    { 
        public int ID { get; set; }

        /// <summary>
        /// title of Article
        /// </summary>
        [Required(ErrorMessage = "Title field is required")]
        public string Title { get; set; }

        /// <summary>
        /// content of Article
        /// </summary>
        [Required(ErrorMessage = "Content required")]
        public string Content { get; set; }

        /// <summary>
        /// keyword witch are associated with article
        /// </summary>
        public string KeywordsInput { get; set; }

        public ICollection<InfoImage> InfoImages { get; set; }

        /// <summary>
        /// Property for the name of the file associated with the article
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Property for the path files
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Used for the name of input fields  
        /// </summary>
        public enum FileType
        {
            uploadFile,
            uploadImage
        }
    }
}
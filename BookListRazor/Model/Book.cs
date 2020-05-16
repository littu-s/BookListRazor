using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookListRazor.Model
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        
        public string Name { get; set; }

        public string Author { get; set; }

        public string ISBN { get; set; }

        //if u need to make changes to a tbl already in Db follow the below steps:
        //Tools -> NuGet Pkg Mngr -> Pkg Mngr Console
        // add-migration <migration_name>
        // update-database
        //this reflects the changes to tbl in DB
    }
}

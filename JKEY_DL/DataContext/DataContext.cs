using JKEY_COMMON.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JKEY_DL.DataContext
{
    public class DataContext: DbContext
    {
        /// <summary>
        /// chuỗi kết nối 
        /// </summary>
        public static string SqlConnectionString;
       
        public DataContext(DbContextOptions<DataContext> options)
                : base(options)
        { }

        public DbSet<Bill> Bill { get; set; }

    }
}

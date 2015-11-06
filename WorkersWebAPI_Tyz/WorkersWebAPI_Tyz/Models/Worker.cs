using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace WorkersWebAPI_Tyz.Models
{
    public class Worker
    {
        [Required(ErrorMessage ="Id is required")]
        public int Id { get; set; }//Model必须提供public的属性，用于json或xml反序列化时的赋值
        [Required(ErrorMessage ="Name is required")]
        [StringLength(18)]
        public string Name { get; set; }
        [Required]
        public string Gender { get; set; }//female or male
        //[Required]
        [Range(18, 60,ErrorMessage ="Age must be between 18-60")]
        public byte? Age { get; set; }//补充：必须是整数?表示可null
        [Required]
        public string Department { get; set; }//指定范围内选择
        [Required]
        public string Position { get; set; }//指定范围内选择

        //[Display(Name = "Employed Date")]
        [DataType(DataType.Date)]//只显示日期，不显示时间
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EmployedDate { get; set; }
    }
    //创建MovieDBContext类来连接数据库WorkersDatabase---tyz
    public class WorkerDBContext : DbContext
    {
        //public MovieDBContext() : base("DefaultConnection") { }
        //https://msdn.microsoft.com/zh-cn/library/gg696460(v=vs.113).aspx介绍DbSet<TEntity>类
        public DbSet<Worker> Workers { get; set; }
    }
}
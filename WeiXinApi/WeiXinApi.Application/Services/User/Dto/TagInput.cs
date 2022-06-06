using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeiXinApi.Application.Services
{
    public class TagInput
    {
        /// <summary>
        /// 标签名称
        /// </summary>
        public string Name { get; set; }
    }


    public class UpdateTagInput : TagInput
    {
        public int Id { get; set; }
    }


    public class UserTagInput : UpdateTagInput
    {
        public List<string> Users { get; set; } = new List<string>();
    }
}

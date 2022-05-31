using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeiXinApi.Application.Services
{
    public class QRInput
    {
        /// <summary>
        /// 二维码类型
        /// </summary>
        public QrCode_ActionName ActionName { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public int ExpireSeconds { get; set; }

        /// <summary>
        /// 回复设置
        /// </summary>
        [Required(ErrorMessage = "回复设置必填")]
        public ReceiveInfo? ReceiveInfo { get; set; }
    }
}

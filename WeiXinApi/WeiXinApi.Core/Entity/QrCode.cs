using Senparc.Weixin.MP;
using SqlSugar;
using System;

namespace WeiXinApi.Core
{
    /// <summary>
    /// 二维码表
    ///</summary>
    [SugarTable("QrCode")]
    public class QrCode
    {
        /// <summary>
        /// id 
        ///</summary>
        [SugarColumn(ColumnName = "Id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// 二维码类型 
        ///</summary>
        [SugarColumn(ColumnName = "ActionName")]
        public QrCode_ActionName ActionName { get; set; }
        /// <summary>
        /// 过期时间 
        ///</summary>
        [SugarColumn(ColumnName = "ExpireSeconds")]
        public int ExpireSeconds { get; set; }
        /// <summary>
        /// SceneId 
        ///</summary>
        [SugarColumn(ColumnName = "SceneId")]
        public int SceneId { get; set; }
        /// <summary>
        /// ticket 
        ///</summary>
        [SugarColumn(ColumnName = "Ticket")]
        public string Ticket { get; set; }
        /// <summary>
        /// 二维码地址 
        ///</summary>
        [SugarColumn(ColumnName = "CodeUrl")]
        public string CodeUrl { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>

        [SugarColumn(ColumnName = "CreatedTime")]
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        [SugarColumn(ColumnName = "ExpiredTime")]
        public DateTime? ExpiredTime { get; set; }

        /// <summary>
        /// 回复设置 
        ///</summary>
        [SugarColumn(ColumnName = "ReceiveInfo", IsJson = true)]
        public ReceiveInfo ReceiveInfo { get; set; }


        /// <summary>
        /// 是否过期
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public bool IsExpired { get; set; } = false;

    }

    public class ReceiveInfo
    {
        /// <summary>
        /// 回复类型:文字，图片等 
        ///</summary>
        public ReceiveType ReceiveType { get; set; }

        /// <summary>
        /// 回复内容 
        ///</summary>
        public string ReceiveString { get; set; }
    }
}

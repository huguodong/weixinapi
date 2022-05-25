using SqlSugar;

namespace WeiXinApi.Core
{
    /// <summary>
    /// 自动回复表
    ///</summary>
    [SugarTable("MessageReceive")]
    public class MessageReceive
    {
        /// <summary>
        /// 主键 
        ///</summary>
        [SugarColumn(ColumnName = "Id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// 回复类型:文字，图片等 
        ///</summary>
        [SugarColumn(ColumnName = "ReceiveType")]
        public ReceiveType ReceiveType { get; set; }
        /// <summary>
        /// 关键字 
        ///</summary>
        [SugarColumn(ColumnName = "KeyWords")]
        public string KeyWords { get; set; }
        /// <summary>
        /// 回复内容 
        ///</summary>
        [SugarColumn(ColumnName = "ReceiveString")]
        public string ReceiveString { get; set; }
    }
}

using System;

namespace CodeGenerator.Core
{

    public class GHotel
    {

        /// <summary>
        /// 
        /// </summary>
        public long HotelId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string HotelName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 酒店状态 1可用,非1不可用
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 按百分比
        /// </summary>
        public int Rate { get; set; }
    }
}


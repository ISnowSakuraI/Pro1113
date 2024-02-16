using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace three.Models
{
    class Products
    {
        public int img_id { get; set; }
        [DisplayName("ชื่อภาพ")]
        public string img_name { get; set; }
        [DisplayName("คำอธิบาย")]
        public string img_description { get; set; }
        [DisplayName("หมวดหมู่")]
        public string img_category { get; set; }
        [DisplayName("เจ้าของ")]
        public string UserName { get; set; }
        [DisplayName("รูป")]
        public string img_url { get; set; }
        [DisplayName("วันที่อัพโหลด")]
        public Nullable<System.DateTime> img_date { get; set; }
    }
    
}

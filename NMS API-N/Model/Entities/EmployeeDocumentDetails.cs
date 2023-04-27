﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NMS_API_N.Model.Entities
{
    public class EmployeeDocumentDetails : FileInfo
    {
#nullable disable
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("EmployeeDocumentMasterId")]
        public EmployeeDocumentMaster EmployeeDocumentMaster { get; set; }
        public int EmployeeDocumentMasterId { get; set; }
     
    }
}

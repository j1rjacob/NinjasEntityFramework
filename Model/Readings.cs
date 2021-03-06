﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class Readings
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get;
            set;
        }

        public string MeterAddress
        {
            get;
            set;
        }

        public DateTime? ReadingDate
        {
            get;
            set;
        }

        public double? ConsumptionL
        {
            get;
            set;
        }

        public int? LowBattaryALR
        {
            get;
            set;
        }

        public int? LeakALR
        {
            get;
            set;
        }

        public int? MagneticTamperALR
        {
            get;
            set;
        }

        public int? MeterErrorALR
        {
            get;
            set;
        }

        public int? BackFlowALR
        {
            get;
            set;
        }

        public int? BrokernPipeALR
        {
            get;
            set;
        }

        public int? EmptyPipeALR
        {
            get;
            set;
        }

        public int? SpecificErrorALR
        {
            get;
            set;
        }
    }
}

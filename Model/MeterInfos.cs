using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class MeterInfos
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string MeterAddress
        {
            get;
            set;
        }

        public string HCN
        {
            get;
            set;
        }

        [DefaultValue(true)]
        public bool IsActive
        {
            get;
            set;
        }
    }
}

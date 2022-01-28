using System.Text;

namespace EntityFrameworkQueries
{
    
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSelectAllVendors_Click(object sender, EventArgs e)
        {
            using APContext dbContext = new();

            // LINQ (Language Integrated Query) method syntax
            List<Vendor> vendorList = dbContext.Vendors.ToList();

            // LINQ query syntax
            List<Vendor> vendorList2 = (from v in dbContext.Vendors
                                       select v).ToList();
        }

        private void btnAllCaliVendors_Click(object sender, EventArgs e)
        {
            using APContext dbContext = new();

            List<Vendor> vendorList = dbContext.Vendors
                                               .Where(v => v.VendorState == "CA")
                                               .OrderBy(v => v.VendorName)
                                               .ToList();

            List<Vendor> vendorList2 = (from v in dbContext.Vendors
                                        where v.VendorState == "CA"
                                        orderby v.VendorName
                                        select v).ToList();
        }

        private void btnSelectSpecificColumns_Click(object sender, EventArgs e)
        {
            using APContext dbContext = new();
            // Anonymous type variable
            List<VendorLocation> results = (from v in dbContext.Vendors
                                            select new VendorLocation
                                            {
                                                VendorName = v.VendorName,
                                                VendorCity = v.VendorCity,
                                                VendorState = v.VendorState
                                            }).ToList();

            StringBuilder displayStr = new();
            foreach (VendorLocation vendor in results)
            {
                displayStr.AppendLine($"{ vendor.VendorName} is in {vendor.VendorCity}");
            }

            // Trying to use method syntax here for specific columns
            /*Same output as above code, but using LINQ method syntax
            
            List<VendorLocation> results2 = dbContext.Vendors
                                            .Select(v => new VendorLocation()
                                            {
                                                VendorName = v.VendorName,
                                                VendorCity = v.VendorCity
                                            })
                                            .ToList();

            StringBuilder displayStr = new();
            foreach (VendorLocation vendor in results2)
            {
                displayStr.AppendLine($"{ vendor.VendorName} is in {vendor.VendorCity}");
            }*/


            MessageBox.Show(displayStr.ToString()); // message box cant't display string builder so .tostring is needed here
        }

        private void btnMiscQueries_Click(object sender, EventArgs e)
        {
            APContext dbContext = new();

            // Check if vendor exists in WA
            bool doesExist = dbContext.Vendors
                             .Where(v => v.VendorState == "WA").Any();

            // Get number of invoices
            int invoiceCount = dbContext.Invoices.Count();
                              
            // Single vendor query
            // single() only returns one element in a sequence, crashes if doesn't exist
            // singleOrDefault() only returns one element in a sequence or null if doesn't exist
            // ? question mark in front of the Vendor says that it could be null and gets rid of green lines
            Vendor? vendor = dbContext.Vendors
                         .Where(v => v.VendorName == "IBM").SingleOrDefault(); 
            if (vendor != null)
            {
                // do something with vendor object
            }
        }
    }

    class VendorLocation
    {
        public string VendorName { get; set; }

        public string VendorCity { get; set; }

        public string VendorState { get; set; }
    }
}
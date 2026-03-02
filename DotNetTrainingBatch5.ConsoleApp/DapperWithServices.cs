using DotNetTrainingBatch5.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTrainingBatch5.ConsoleApp
{
    public class DapperWithServices
    {
        private readonly string _connection = "Data Source=.;Initial Catalog=DotNetTrainingBatch5;User Id=sa;Password=p@ssw0rd;TrustServerCertificate=True";
        private readonly AdoDotNetServices _dapservice;

        public DapperWithServices()
        {
            _dapservice = new DapperServices(_connection);
        }
    }
}

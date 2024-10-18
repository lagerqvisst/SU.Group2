using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU.Backend.Models.Enums.Insurance
{
    //Represents "Prisbasbelopp 2024 in Sweden".
    public enum Deductible
    {
        Full = 57300,
        ThreeQuarter = 42975,
        Half = 28650,
        Quarter = 14325,

    }
}

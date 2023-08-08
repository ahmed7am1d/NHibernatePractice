using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernetCreateSession.Models;
public class Wallet
{
    public virtual int Id { get; set; }
    //I am sure that holder is required and exists
    public virtual string Holder { get; set; } = null!;
    public virtual decimal Balance { get; set; }

    public override string ToString()
    {
        return $"[{Id}] {Holder} ({Balance:C})";
    }
}

using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;


namespace NHibernetCreateSession.Models;
public class WalletMapping : ClassMapping<Wallet>
{
    public WalletMapping()
    {
        //We do the configurations in the constructor
        //[1]- Define the primary key with its necessary configurations
        Id(x => x.Id, y =>
        {
            y.Generator(Generators.Identity);
            y.Type(NHibernateUtil.Int32);
            y.Column("Id");
            y.UnsavedValue(0);
        });

        //[2]- Define the proprties 
        Property(p => p.Holder, y =>
        {
            y.Length(50);
            //VARCHAR=> AnsiString
            //NVARCHAR => string 
            y.Type(NHibernateUtil.AnsiString);
            y.NotNullable(true);
        });

        Property(p => p.Balance, y =>
        {
            y.Type(NHibernateUtil.Decimal);
            y.NotNullable(true);
        });

        //[3]- Table
        Table("Wallets");
    }
}

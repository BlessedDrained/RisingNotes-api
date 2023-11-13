using System.ComponentModel.DataAnnotations.Schema;

namespace MainLib.Dal.Model.Base;

///
public abstract record DalModel<TKey> : IDalModel<TKey>
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public TKey Id { get; set; }
}
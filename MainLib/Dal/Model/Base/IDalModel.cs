namespace MainLib.Dal.Model.Base;

public interface IDalModel<TKey>
{
    TKey Id { get; set; }
}
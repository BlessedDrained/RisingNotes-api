// using Dal.BaseUser;
// using Dal.Song;
// using MainLib.Dal.Model.Base;
//
// namespace Dal.FeatAuthor;
//
// /// <summary>
// /// Модель соавтора
// /// </summary>
// public record FeatAuthorDal : DalModel<Guid>
// {
//     /// <summary>
//     /// Псевдоним
//     /// </summary>
//     public string Name { get; set; }
//
//     /// <summary>
//     /// Нав свойство
//     /// </summary>
//     public UserDal AuthorModel { get; set; }
//
//     /// <summary>
//     /// Нав свойство
//     /// </summary>
//     public Guid? AuthorModelId { get; set; }
//
//     /// <summary>
//     /// Список песен, в которых проходят как featured
//     /// </summary>
//     public List<SongDal> SongList { get; set; }
// }
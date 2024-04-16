﻿using Dal.Context;
using MainLib.Dal.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace Dal.ShortVideo.Repository;

/// <inheritdoc cref="IShortVideoRepository"/>
public class ShortVideoRepository : Repository<ShortVideoDal, Guid>, IShortVideoRepository
{
    public ShortVideoRepository(ApplicationContext context) : base(context)
    {
    }
}
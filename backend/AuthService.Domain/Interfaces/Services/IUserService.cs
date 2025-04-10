﻿using System.Linq.Expressions;

namespace AuthService.Domain;

public interface IUserService
{
    Task<UserEntity> AddAsync(AddUser addUser, CancellationToken cancellationToken = default);

    Task BlockAsync(Guid userGuid, CancellationToken cancellationToken = default);

    Task UnblockAsync(Guid userGuid, CancellationToken cancellationToken = default);

    Task ChangePasswordAsync(Guid userGuid, string newPassword, CancellationToken cancellationToken = default);

    Task<IEnumerable<UserEntity>> GetAsync(Expression<Func<UserEntity, bool>> predicate,
        CancellationToken cancellationToken = default);
}
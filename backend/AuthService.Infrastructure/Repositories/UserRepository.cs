using System.Linq.Expressions;
using AuthService.Domain;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Repositories;

public class UserRepository(Context context) : IUserRepository
{
    public async Task<UserEntity> AddAsync(UserEntity user, CancellationToken cancellationToken = default)
    {
        await context.Users.AddAsync(user, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return user;
    }

    public async Task<UserEntity> UpdateAsync(UserEntity user, CancellationToken cancellationToken = default)
    {
        var userToUpdate =
            await context.Users.FirstOrDefaultAsync(f => f.Guid == user.Guid, cancellationToken: cancellationToken);

        if (userToUpdate != null)
        {
            userToUpdate = user;

            await context.SaveChangesAsync(cancellationToken);
        }

        return user;
    }

    public async Task<IEnumerable<UserEntity>> GetAsync(Expression<Func<UserEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await context.Users.Where(predicate).ToListAsync(cancellationToken);
    }
}
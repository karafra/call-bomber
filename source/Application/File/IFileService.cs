using DotNetCore.Objects;

namespace Architecture.Application;

public interface IFileService
{
    Task<IEnumerable<BinaryFile>> AddAsync(string directory, IEnumerable<BinaryFile> files);

    Task<BinaryFile> GetAsync(string directory, Guid id);
}

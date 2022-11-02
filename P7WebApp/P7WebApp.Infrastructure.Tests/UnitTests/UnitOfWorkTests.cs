using Moq;
using P7WebApp.Application.Common.Interfaces;
using System;

public class UnitOfWorkTests
{
	[Fact]
	public void UnitOfWork_WillCallCommitChanges_Succesfully()
	{
		var mockContext = new Mock<IApplicationDbContext>();
	}
}

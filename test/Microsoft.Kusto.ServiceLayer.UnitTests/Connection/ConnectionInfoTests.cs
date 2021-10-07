using System;
using Microsoft.Kusto.ServiceLayer.Connection;
using Microsoft.Kusto.ServiceLayer.Connection.Contracts;
using Microsoft.Kusto.ServiceLayer.DataSource;
using Microsoft.SqlTools.ServiceLayer.Connection.ReliableConnection;
using Moq;
using NUnit.Framework;

namespace Microsoft.Kusto.ServiceLayer.UnitTests.Connection
{
    public class ConnectionInfoTests
    {
        [TestCase("")]
        [TestCase(null)]
        public void AddConnection_Throws_Exception_For_Invalid_ConnectionType(string connectionType)
        {
            var connectionInfo = new ConnectionInfo("", new ConnectionDetails());
            Assert.Throws<ArgumentException>(() => connectionInfo.AddConnection(connectionType, null));
        }

        [TestCase("")]
        [TestCase(null)]
        public void GetConnection_Throws_Exception_For_Invalid_ConnectionType(string connectionType)
        {
            var connectionInfo = new ConnectionInfo( "", new ConnectionDetails());
            Assert.Throws<ArgumentException>(() => connectionInfo.TryGetConnection(connectionType, out _));
        }

        [Test]
        public void AddConnection_And_GetConnection_AddAndGet()
        {
            var connectionInfo = new ConnectionInfo("", new ConnectionDetails());

            var dataSourceFactoryMock = new Mock<IDataSourceFactory>();
            var reliableDataSource = new ReliableDataSourceConnection(new ConnectionDetails(), RetryPolicyFactory.NoRetryPolicy,
                RetryPolicyFactory.NoRetryPolicy, dataSourceFactoryMock.Object, "");
            connectionInfo.AddConnection("ConnectionType", reliableDataSource);

            connectionInfo.TryGetConnection("ConnectionType", out var connection);
            Assert.AreEqual(reliableDataSource, connection);
        }

        [TestCase("")]
        [TestCase(null)]
        public void RemoveConnection_Throws_Exception_For_Invalid_ConnectionType(string connectionType)
        {
            var connectionInfo = new ConnectionInfo("", new ConnectionDetails());
            Assert.Throws<ArgumentException>(() => connectionInfo.RemoveConnection(connectionType));
        }

        [Test]
        public void RemoveConnection_Removes_Connection()
        {
            var connectionInfo = new ConnectionInfo("", new ConnectionDetails());

            var dataSourceFactoryMock = new Mock<IDataSourceFactory>();
            var reliableDataSource = new ReliableDataSourceConnection(new ConnectionDetails(), RetryPolicyFactory.NoRetryPolicy,
                RetryPolicyFactory.NoRetryPolicy, dataSourceFactoryMock.Object, "");
            connectionInfo.AddConnection("ConnectionType", reliableDataSource);

            connectionInfo.RemoveConnection("ConnectionType");

            connectionInfo.TryGetConnection("ConnectionType", out var connection);
            Assert.IsNull(connection);
        }

        [Test]
        public void RemoveAllConnections_RemovesAllConnections()
        {
            var connectionInfo = new ConnectionInfo("", new ConnectionDetails());

            var dataSourceFactoryMock = new Mock<IDataSourceFactory>();
            var reliableDataSource = new ReliableDataSourceConnection(new ConnectionDetails(), RetryPolicyFactory.NoRetryPolicy,
                RetryPolicyFactory.NoRetryPolicy, dataSourceFactoryMock.Object, "");
            connectionInfo.AddConnection("ConnectionType", reliableDataSource);

            connectionInfo.RemoveAllConnections();

            connectionInfo.TryGetConnection("ConnectionType", out var connection);
            Assert.IsNull(connection);
        }
    }
}
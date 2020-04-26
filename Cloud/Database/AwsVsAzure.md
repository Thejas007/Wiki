# Azure vs. AWS RDS: Which Service Is Right for Your Cloud Database Requirements?

Source : https://logz.io/blog/azure-vs-aws/

# Database Engines
RDS supports six database engines, Amazon Aurora, PostgreSQL, MySQL, MariaDB, Oracle, and Microsoft SQL Server whereas SQL Database is a service exclusively based on Microsoft SQL Server.

PostgreSQL, MySQL, MariaDB, Oracle, and Microsoft SQL Server are hosted by AWS on Elastic Block Store (EBS) volumes. As Amazon’s own proprietary database engine, Aurora uses a different storage infrastructure and is designed to address some of the scaling and replication issues associated with traditional databases.

# Resource Allocation
RDS works on the same lines as EC2, using the concept of instances to allocate compute resources to your databases. You then provision storage capacity separately. However, in the case of Aurora, storage scales automatically and you only pay for what you actually consume.

By contrast, SQL Database works on a tier system in which each tier is suited to different types of workloads ranging from small-scale development/testing environments to high-transactional, mission-critical applications. Each tier is broken down further into different performance levels ranked by Microsoft’s own unit of measure for resource capability known as the Database Transaction Unit (DTU).

In addition to its Single Database pricing model, Microsoft recently introduced Elastic Pools, which provides a collective resource for database hosting. This helps users to address fluctuations in load by spreading resources across several databases, thereby maximizing utilization and reducing costs.

# Scaling
You can vertically scale an RDS or SQL Database deployment via either the vendor’s console or a simple API call. In the case of SQL Database, you can also scale up using a T-SQL ALTER DATABASE statement.

AWS charges storage separately from compute. Standard RDS provides up to a maximum of 6TB of storage. However, it has no automatic resizing capability. Aurora, on the other hand, is more flexible and scales automatically in 10GB increments up to a maximum of 64TB of storage.

With SQL Database, storage is included in the price of your selected tier and performance level. The service permits a maximum database size of 1TB and up to 2.9TB total storage per elastic pool. However, individual limits apply within each tier.

RDS supports read-only horizontal scaling, by which you’re able to add replicas to improve query performance. By contrast, Microsoft Azure advocates a sharding approach using its Elastic Database tools.

# Other Features
Both RDS and SQL Database provide point-in-time backups, which you can use to return a database to an earlier state.

Many RDS DB instance types include Provisioned IOPS (PIOS) — a feature designed to improve I/O between your database instance and storage. RDS can also be launched in Amazon Virtual Private Cloud (VPC) whereas Database SQL doesn’t yet support a virtual private network (VPN).

One of Microsoft’s notable SQL Database features is support for the hybrid cloud service Stretch Database, which migrates cold data from your on-premise SQL Server. However, it still allows you to query your data stretched to Azure while streamlining your on-premise database at the same time.

# NoSQL DBaaS
DynamoDB is currently Amazon’s only NoSQL DBaaS designed for storing data at scale whereas Microsoft offers two distinct products, DocumentDB and Table Storage.

# Database Models
DynamoDB and DocumentDB are based on the document store database model. They are similar in nature to open-source solutions MongoDB and CouchDB and are built to accept JSON structures.

Table Storage, on the other hand, is a key-value store and works on the same principle as Redis and Couchbase. In other words, it uses a database model that’s fundamentally very similar to DynamoDB and DocumentDB but without any constraints on document structure. It is therefore used for storing data rather than querying or processing it.

Table Storage also enforces strong data consistency. This ensures that the latest version of your data is always returned, which is essential in systems where concurrent users simultaneously access and update the same information.

With DynamoDB, you specify whether you want eventually consistent or strongly consistent reads. DocumentDB gives you four options: strong consistency, eventual consistency and two intermediate levels that offer an alternative consistency compromise.

# Scaling
Both DynamoDB and DocumentDB are virtually infinitely scalable. Table Storage is limited to a maximum of 500TB per account, although you can extend your capacity by partitioning your data objects across accounts.

Table Storage is the only DBaaS with native auto-scaling capability. However, a number of third-party solutions are available that can monitor and scale DynamoDB and DocumentDB automatically.

# Query Support
DocumentDB is the only one of the three DBaaS offerings that comes with full-blown SQL functionality. However, you can integrate DynamoDB with Elasticsearch using the DynamoDB Logstash plugin to enable free-text search of content such as messages, tags, and keywords.

Amazon also provides another NoSQL service, SimpleDB, which works along a similar line to Table Storage. However, databases are limited to 10GB in size, making the service unsuitable for Big Data use cases.

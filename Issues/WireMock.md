

# Error while accessing Log Entries : Collection was modified; enumeration operation may not execute.

 - When access FluentMockServer.LogEntries in version  WireMock.Net.1.0.5 you will get enumeration error because concurrent updated of collection.  This as been fixed in later version.
// Source  public IEnumerable<ILogEntry> LogEntries => new ReadOnlyCollection<LogEntry>(_options.LogEntries.ToList());
 -  https://github.com/WireMock-Net/WireMock.Net/issues/308

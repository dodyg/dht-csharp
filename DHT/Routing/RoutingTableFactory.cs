﻿namespace DHT.Routing
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Nodes;

    /// <summary>
    /// Factory for constructing the routing table
    /// </summary>
    public class RoutingTableFactory
    {
        public static IRoutingTable FromFile(string routingTablePath)
        {
            var lines = File.ReadAllLines(routingTablePath);
            var nodes = new List<NodeInfo>();

            foreach (var line in lines)
            {
                var tokens = line.Split(' ');
                var nodeId = UInt32.Parse(tokens[0]);
                var hostName = tokens[1];
                var port = int.Parse(tokens[2]);

                var nodeInfo = new NodeInfo()
                {
                    NodeId = nodeId,
                    HostName = hostName,
                    Port = port
                };

                nodes.Add(nodeInfo);
            }

            var hashGenerator = new Sha256HashGenerator();
            var routingTable = new RoutingTable(hashGenerator, nodes);

            return routingTable;
        }
    }
}

using System.Net.Sockets;
using System.Text;

try
{
    // Create a socket and connect to the Redis server
    var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    socket.Connect("127.0.0.1", 6379);

    // Create a buffered stream for the response
    var responseStream = new BufferedStream(new NetworkStream(socket), 1024);

    // Create a Redis command to request a value
    var requestString = "*2\r\n$3\r\nGET\r\n$4\r\nfood\r\n";
    byte[] request = Encoding.UTF8.GetBytes(requestString);
    socket.Send(request);

    // Read the first line of the response to get the value length
    var result = new StringBuilder();
    int b;
    while ((b = responseStream.ReadByte()) != -1)
    {
        if (b == '\r')
        {
            responseStream.ReadByte(); // Read the '\n' character
            break;
        }
        result.Append((char)b);
    }

    // Check if the response indicates no value found
    if (result.ToString() == "$-1")
        Console.WriteLine("Result: (nil)"); // No value found
    else
    {
        // Parse the response length and retrieve the value
        if (!int.TryParse(result.ToString()[1..], out int responseLength))
        {
            Console.WriteLine("Invalid response length format");
            responseStream.Close();
            socket.Close();
            return;
        }

        // Read the value bytes and convert to string
        var responseValue = new byte[responseLength];
        responseStream.Read(responseValue, 0, responseLength);
        string responseString = Encoding.UTF8.GetString(responseValue);
        Console.WriteLine("Result: " + responseString);
    }

    // Close the response stream and socket
    responseStream.Close();
    socket.Close();
}
catch (SocketException se)
{
    Console.WriteLine("SocketException: " + se.Message);
}
catch (FormatException fe)
{
    Console.WriteLine("FormatException: " + fe.Message);
}
catch (Exception e)
{
    Console.WriteLine("An error occurred: " + e.Message);
}

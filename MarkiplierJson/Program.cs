// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;
using MarkiplierJson.models;
using Newtonsoft.Json;
using YoutubeExplode;


var fetched = new List<string>();

var entrypoint = "j64oZLF443g";

var choiceGroups = new List<ChoiceGroup>();

var queue = new Queue<string>();

queue.Enqueue(entrypoint);

var yt = new YoutubeClient();

// Put Out the Fire ► https://youtu.be/raIqPgW-quI

while (queue.Count > 0)
{
    var currentCode = queue.Dequeue();

    if (fetched.Contains(currentCode))
    {
        continue;
    }

    fetched.Add(currentCode);
    

    Console.WriteLine(currentCode);

    var result = await yt.Videos.GetAsync(currentCode);
    Console.WriteLine(result.Title);
    
    var desc = result.Description;

    // split lines
    var lines = desc.Split('\n');
    // filter lines that contain "►" and contain "https://youtu.be/"
    var linkRows = lines.Where(x => x.Contains("►") && x.Contains("https://youtu.be/"));
    // split lines on "►"

    var choices = new List<Choice>();

    foreach (var l in linkRows)
    {
        var linkSplits = l.Split('►');
        var choiceText = linkSplits[0].Trim();

        var link = linkSplits[1].Trim();
        // get last 11 characters
        var newCode = link.Substring(link.Length - 11);

        if (!fetched.Contains(newCode))
        {
            queue.Enqueue(newCode);
        }

        choices.Add(new Choice()
        {
            text = choiceText,
            nextChoiceGroupWatchCode = newCode,
        });
    }
    

    choiceGroups.Add(new ChoiceGroup()
    {
        videoTitle = result.Title,
        watchCode = currentCode,
        type = currentCode == "j64oZLF443g" ? "start" : choices.Count < 1 ? "end" : "default",
        showAt = result.Duration?.TotalSeconds - 10 ?? 10,
        ending = choices.Count < 1 ? new Ending() : null,
        choices = choices
    });
    
    

    Console.WriteLine($"Total count: {choiceGroups.Count}");
    Console.WriteLine($"-----------------------------------------------------");

}

Console.WriteLine($"-----------------------------------------------------");
// print json
Console.WriteLine(JsonConvert.SerializeObject(choiceGroups));



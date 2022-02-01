using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml;

namespace demo_octocat.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public string octoImage { get; private set; } 

    public void OnGet()
    {
        String URLString = "https://octodex.github.com/atom.xml";

        using var reader = XmlReader.Create(URLString);

        reader.ReadToFollowing("entry");
        List<string> octolist = new List<string>();

        do
        {
            reader.ReadToFollowing("content");
            string content = reader.ReadElementContentAsString();
            
            string test = content.Substring(content.IndexOf("<img src=") + "<img src=".Length, content.IndexOf("/>") - content.IndexOf("<img src=") - "<img src=".Length);
            string imgUrl = test.Replace(@"""","");
            octolist.Add(imgUrl);

        } while (reader.ReadToFollowing("entry"));


            Random rnd = new Random();

            for (int j = 0; j < octolist.Count(); j++)
            {
               octoImage = octolist[rnd.Next(octolist.Count())];
               break;
            }


    }

}

//------------------------------------------------------------------------------
//AUTHORS: ASHFAQ AHAMED


using ArchestrA.GRAccess;
using System;
using System.Collections;
using System.IO;
//using Microsoft.Office.Interop.Excel;
using System.Linq;
using System.Text;


namespace LinkGraphicToTemplate
{
    class Program
    {
        [STAThread]
        static void Main()

        {
            GRAccessApp grAccess = new GRAccessAppClass();
            Console.WriteLine("==================================================");
            Console.WriteLine("       GRAccess Graphic Linker Tool v1.0");
            Console.WriteLine("==================================================");
            Console.WriteLine();
            Console.WriteLine("This application allows you to link graphical symbols");
            Console.WriteLine("to templates or instances in an ArchestrA Galaxy.");
            Console.WriteLine();
            Console.WriteLine("Features:");
            Console.WriteLine(" - Query templates or instances by name");
            Console.WriteLine(" - Automatically detect $ symbols for instances");
            Console.WriteLine(" - Check out templates, link graphics, and save changes");
            Console.WriteLine();
            Console.WriteLine("==================================================");
            Console.WriteLine();
            Console.WriteLine("GR Access Initiated... ");

            Console.WriteLine("Enter NODE name TO CONTINUE:");
            string nodeName = Console.ReadLine(); 


            Console.WriteLine("Enter GR name TO CONTINUE:");
            string galaxyName = Console.ReadLine(); //"SG06_

            Console.WriteLine();
            Console.WriteLine($"Galaxy - {galaxyName} -  is connected.");
            Console.WriteLine();
            Console.WriteLine("Processing.... Please wait......");

            IGalaxies gals = grAccess.QueryGalaxies(nodeName);
            ICommandResult cmd;
            IGalaxy galaxy = gals[galaxyName];
            galaxy = grAccess.QueryGalaxies(nodeName)[galaxyName];
            // log in
            galaxy.Login("ADMINISTRATOR", "");
            cmd = galaxy.CommandResult;
            if (!cmd.Successful)
            {
                Console.WriteLine("Login to galaxy Failed :" +
                                cmd.Text + " : " +
                                cmd.CustomMessage);
                return;
            }
            else
            {
                Console.WriteLine(galaxyName + " login successfull, do not close the popup");
                Console.WriteLine();
            }



            // get the taglist template // can be done in excel also
            string Templatelist = @"c:\InternalScripts\LinkSymbol\templatesymbollinking\template_list.txt";
            string Symbollist = @"c:\InternalScripts\LinkSymbol\templatesymbollinking\symbol_list.txt";

            string[] temList = File.ReadLines(Templatelist).ToArray();
            string[] SymList = File.ReadLines(Symbollist).ToArray();

            var temTlineCount = File.ReadAllLines(Templatelist).Length;
            var symTlineCount = File.ReadAllLines(Symbollist).Length;

            if (temTlineCount == symTlineCount)
            {
                Console.WriteLine("Both file line count matched successfully");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine(" Match unsuccessfull");
                Console.WriteLine();
                return;
            }


            //verify line count
            Console.WriteLine("No.of tags in new Tag File: " + symTlineCount);
            Console.WriteLine();


            Console.WriteLine("Linking loop Started");
            Console.WriteLine();


            for (int i = 0; i < symTlineCount; i++)
            {
                //get template name from notepad file
                string TempName = temList[i];
                Console.Write("Processing:" + " " + i + " " + temList[i]);
                Console.WriteLine();

                //string TempName = "$testTemp";

                
                string[] refobjTempName = new string[] { TempName };

                //get symbol name from notepad file
                string SymName = SymList[i];

                string[] refobjSymName = new string[] { SymName };

                //The link name that is to be given in the template
                string reflinkedsym = SymName +"_linkedsym";

                //IgObjects queryResult = galaxy.QueryObjectsByName(EgObjectIsTemplateOrInstance.gObjectIsTemplate,ref refobj);
                Console.WriteLine($"refobj contains: {string.Join(", ", refobjTempName)}");


                // Query the template collection
                IgObjects template2 = null;

                //--------------check is template or instance---------------//
                if (TempName.Contains("$"))
                {
                    template2 = galaxy.QueryObjectsByName(EgObjectIsTemplateOrInstance.gObjectIsTemplate, ref refobjTempName);
                    if (template2 == null || template2.count == 0)
                    {
                        Console.WriteLine($"Template '{TempName}' not found.");
                        return;
                    }
                    else
                    {
                        Console.WriteLine($"Template '{TempName}' found in the Galaxy.");
                    }
                }
                else
                {
                    template2 = galaxy.QueryObjectsByName(EgObjectIsTemplateOrInstance.gObjectIsInstance, ref refobjTempName);
                    if (template2 == null || template2.count == 0)
                    {
                        Console.WriteLine($"Template '{TempName}' not found.");
                        return;
                    }
                    else
                    {
                        Console.WriteLine($"Template '{TempName}' found in the Galaxy.");
                    }
                }


                ArchestrA.GRAccess.IgObject template = null;

                // Safely get the template using GetObject
                for (int j = 1; j <= template2.count; j++) // 1-based index
                {
                    var obj = template2[j]; // obj is COM object
                    if (obj.Tagname == TempName) // match exact name
                    {
                        template = obj as ArchestrA.GRAccess.IgObject;
                        if (template != null)
                            break;
                    }
                }

                //check
                if (template == null)
                {
                    Console.WriteLine($"Failed to retrieve template '{TempName}' as IGObject.");
                    return;
                }
                Console.WriteLine($"Igobject Template converted");
                Console.WriteLine($"Template '{TempName}' successfully loaded.");


                // --- Check out the template before editing ---
                template.CheckOut();
                Console.WriteLine($"Template '{TempName}' checked out for editing.");

                // --- Link the graphic to the template ---
                // symbolname, reference given in template
                template.AddLinkedSymbol(SymName, reflinkedsym);

                // --- Save changes ---
                template.Save();
                Console.WriteLine($"Linked '{SymName}' to template '{TempName}' successfully.");

                // --- Optional: Check in the template ---
                template.CheckIn();
                Console.WriteLine($"Template '{TempName}' checked in and changes committed.");

                //end
                Console.WriteLine($"✅ Row {i} - Successfully linked '{SymName}' to the template '{TempName}'!");
            }
        }
    }
}
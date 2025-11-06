GRAccess Graphic Linker
Overview

The GRAccess Graphic Linker is a lightweight C# console application built to automate the process of linking ArchestrA graphics to object templates within a Wonderware / AVEVA Galaxy.
It leverages the ArchestrA GRAccess API to simplify repetitive engineering tasks that would otherwise require manual configuration in the Integrated Development Environment (IDE).

Purpose

In large-scale automation projects, engineers often need to:

Create or update multiple templates and their corresponding graphics.

Maintain consistent symbol references across templates.

Reduce manual steps and potential errors during bulk configuration.

This tool enables users to perform these operations quickly and safely from a script, ensuring consistency and repeatability.

Key Features

🔹 Connects to a Galaxy Repository (GR) using the GRAccess API.

🔹 Authenticates with the specified Galaxy using administrator credentials.

🔹 Searches for templates or instances dynamically, based on user input.

🔹 Automatically checks out templates, links symbols, saves, and checks them back in.

🔹 Supports validation for names containing $ (system or derived templates).

🔹 Displays clear console feedback for each operation.

How It Works

The application prompts for:

Node Name (the system hosting the Galaxy Repository Service)

Galaxy Name (target ArchestrA Galaxy)

The app connects and logs in to the Galaxy.

Based on user input, it:

Queries for the desired template or instance.

Checks out the object for editing.

Links a specified graphic symbol.

Saves and checks in the changes.

Completion and error details are displayed on the console.

System Requirements

Operating System: Windows 10 / 11

Software: AVEVA System Platform (GRAccess API installed)

.NET Framework: 4.7.2 or later

Dependencies:

ArchestrA.GRAccess.dll (part of AVEVA System Platform installation)

Installation

Copy the executable (LinkGraphicToTemplate.exe) to your desired location.

Ensure ArchestrA GRAccess is installed on the same machine.

Run the program from a command prompt (with administrator rights).

Disclaimer

This tool interacts directly with the Galaxy Repository.
It is recommended to test in a development environment before using it in production.
Use with care to avoid unintended modifications to templates.


Hands on steps:
1.	Go the below folder
a.	 <img width="975" height="188" alt="image" src="https://github.com/user-attachments/assets/4b959c1a-1f53-4317-9c3a-8ca265d4470e" />


i.	Template_list.txt – list all template/instance here (Template name with $ symbol and instance name as i)
ii.	Example below for 5 mix of temp/inst
  <img width="689" height="431" alt="image" src="https://github.com/user-attachments/assets/5ca6c3e6-8c34-40a6-8a58-4caeae19ed2f" />


iv.	Symbol_list.txt – list the corresponding symbol (row wise) here
<img width="723" height="416" alt="image" src="https://github.com/user-attachments/assets/28d374ab-7597-4d11-90ee-048c16a26bbc" />


2.	Go to this app
a.	 <img width="552" height="316" alt="image" src="https://github.com/user-attachments/assets/88212877-e806-4d48-9d0d-e1b2e1e612d6" />

3.	Key in the server/gr details with credentials
a.	<img width="975" height="513" alt="image" src="https://github.com/user-attachments/assets/b3168806-bba3-4672-8255-382d3f86e797" />


4.	It will automatically link the symbol to the corresponding template given in the txt file.
a.	<img width="919" height="1350" alt="image" src="https://github.com/user-attachments/assets/8190eb46-116a-4d68-9d4f-fc3bcaf95322" />


# :book: CalculatorAppMAP

# :pushpin: About the Project
This project is a WPF-based Calculator Application developed in C#. It features both Standard and Programming Modes, allowing users to perform basic arithmetic operations, advanced calculations like square roots and percentages, and base conversions (binary, octal, decimal, hexadecimal). The calculator supports keyboard shortcuts, memory functions, and clipboard operations (copy, cut, paste). Additionally, it includes a complex expression evaluator and saves user preferences (mode and last used base). The application follows the MVVM (Model-View-ViewModel) pattern for better separation of concerns and maintainability.

# :hammer_and_wrench: Functionalities
 * :1234: Basic Arithmetic operations: Perform basic calculations like addition, subtraction, multiplication, and division.
 * :symbols: Advanced Mathematical Functions:Square,Square Root,Inverse,Chnage Sign,Percentage
 * :repeat: Memory functions:Memory Sotre,Recall,Add,Substract,Clear
 * :1234: Base conversions:Supports number base conversions between binary,octal,decimal and hexadecimal.The initial base when the applicattion is on is base 10 and can be easily switched to other bases.
 * :1234: Digit Grouping:Toggle grouping of digits in large numbers for better readability (e.g., separating thousands with commas). based on your location
 * :1234: Comnplex Expression Evaluation:Users can input complex mathematical expressions and evaluate them. This is done through a popup input dialog that allows for flexible calculations.
 * :abcd: Keyboard Shortcuts:Supports keyboard input for all digit and operator buttons, providing an efficient way to use the calculator without relying on the mouse.
 * :notebook_with_decorative_cover: Clipboard Integration: Copy, Cut, and Paste functionalities for easy text manipulation from the display.
 * :twisted_rightwards_arrows: Mode Switching:The applicattions has two modes,Standard and Programming mode
 * :smiley: User Preferences:The application remembers the user’s last used mode (Standard or Programming) and the last selected base, saving these preferences between sessions.


# :books: Project Structure 
  📂 CalculatorProject  
 ┣ 📂 Properties 
 * ┃ ┣ Settings.settings
 ┣ 📂 Model 
 ┃ ┣ 📜 Calculator.cs  
 ┣ 📂 View
 ┃ ┣ 📜 About.xaml
 ┃ ┣ 📜 About.xaml.cs
 ┃ ┣ 📜 Calculator.xaml
 ┃ ┣ 📜 Calculator.xaml.cs
 ┃ ┣ 📜 MemoryWindow.xaml
 ┃ ┣ 📜 MemoryWindow.xaml.cs
 ┣ 📂 View
 ┃ ┣ 📜 Calculator.xaml
 ┃ ┣ 📜 Calculator.xaml.cs
 ┣ 📜 README.md  
 ┣ 📜 .gitignore  

# :gear: Design and Arhitecture
The project is organized on MVVM (Model-View-Controller) design pattern:
 * Model: Calculator.cs 
 * View: About.xaml,Calculator.xaml,MemoryWindow.xaml
 * ViewModel: Calculator.cs
   
# :rocket: Installation and run the project
  1. Make sure you clone the repo or download it
  2. Open it in Visual Studio or Visual Studio Code,depinding on your preferences
  3. If you want to run it on VS code ,make sure to run dotnet run in the terminal

# :desktop_computer: Technologies and Libraries used:
1. [C#](https://dotnet.microsoft.com/en-us/languages/csharp)-the main language
2. [WPF]([https://kivy.org/](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/overview/?view=netdesktop-9.0))-Framework for GUI
3. [Visual Studio](https://visualstudio.microsoft.com/)

# :keyboard: Author
 This project is developed by `Negoita Andrei` and it was a homework which was later developed into some more functionalities

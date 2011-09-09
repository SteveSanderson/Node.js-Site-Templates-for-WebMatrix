Want to build [Node.js](http://nodejs.org) applications on Windows using [WebMatrix](http://www.microsoft.com/web/webmatrix/)? This project gives you Site Templates for Node.js applications.

# Installation

1. **Install [WebMatrix](http://www.microsoft.com/web/webmatrix/).** This is a free, lightweight IDE from Microsoft, that can be used to build sites with ASP.NET, PHP, Node.js, and more.
1. **Follow [Tomasz's instructions to install IISNode for IIS Express](http://tomasz.janczuk.org/2011/08/developing-nodejs-applications-in.html).** Read the instructions carefully and be sure to use a 32-bit command prompt when running `install_iisexpress.bat`, as the instructions specify.
1. **Download and run the [Node.js Site Templates for WebMatrix installer](https://github.com/SteveSanderson/Node.js-Site-Templates-for-WebMatrix/downloads).**

# Usage

Once you've installed the above three items, you can launch WebMatrix (from your Start menu), choose to create a **Site from Template**, and you'll be able to pick from these Node.js templates:

* **Empty Node.js Site** - start with a minimal, single-file application, and no dependencies. By default it just sends the string `Hello world` to the browser.
* **Node.js Express Site** - use [Express](http://expressjs.com/), an MVC-style application framework, to create a site with routing and templating. Currently this template sets you up with the [Jade](https://github.com/visionmedia/jade) view engine by default, and gives you a simple starting point with a "Home" and an "About" page.

To run your site, choose "Run" from the WebMatrix toolbar.
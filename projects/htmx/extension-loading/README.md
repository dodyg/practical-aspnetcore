# htmx 4 extension loading

Load extension scripts directly after htmx; htmx 4 removed `hx-ext`. An `extensions` meta config can restrict registration, and custom extensions call `htmx.registerExtension(name, methodMap)`.

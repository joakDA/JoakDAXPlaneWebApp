# JoakDAXPlaneWebApp

XPlane Web App is a web application that has been developed as part of the final postgraduate project for the Master in Website Management and Engineering taught by the International University of La Rioja.

XPlane Web App connects with the X-Plane 11 flight simulator to receive and process flight data.

## Installation

The installation process includes two steps. First of all, we must install the backend side (ASP.NET Core) and all its requirements.
Then, the client side app developed with Angular 10.

The application has been tested on Linux machines with Debian 10 installed. The installation process may differ on other Linux distributions, but it may work too.
### Backend side
Documentation about the installation steps could be found here:
https://docs.microsoft.com/en-us/dotnet/core/install/linux-debian
1. Install ASP.NET Core signing package and the repository:
```
wget https://packages.microsoft.com/config/debian/10/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb
```
2. Update repository list and install .NET Core Runtime and other required packages:
```
sudo apt-get update
sudo apt-get install -y apt-transport-https git
sudo apt-get update
sudo apt-get install -y aspnetcore-runtime-5.0
```
3. Install Apache webserver that will work as reverse proxy server and enable mod_proxy
```
sudo apt update
sudo apt install apache2
sudo a2enmod proxy_http
sudo a2enmod headers
```
4. Create website directory copying the published files there, set owner and permissions.
```
mkdir /var/www/joakda_xp_webapp
chown -R www-data:www-data /var/www/joakda_xp_webapp/
# Then, copy files using scp or your favorite SFTP client (I have uploaded to /home/joaquin directory, so I need to copy then)
cp -R /home/joaquin/JoakDAXPWebApp_v1_0_0_0/* /var/www/joakda_xp_webapp/
find /var/www/joakda_xp_webapp/ -type d -exec chmod ug+rwxs {} \;
find /var/www/joakda_xp_webapp/ -type f -exec chmod ug+rw,o+r {} \;
```
5. Change Apache listening port and configure virtual host
5.1 Change apache port
````
sudo nano /etc/apache2/ports.conf
````
Configure the port to one of your choice (for example, 8080).
5.2 Virtual host:

Create a file with the command and copy the code to configure virtual host:
````
sudo nano /etc/apache2/sites-available/joakdaxpwebapp.local.conf
````
````
<VirtualHost *:8080> 
    RequestHeader set "X-Forwarded-Proto" expr=%{REQUEST_SCHEME} 
    ProxyPreserveHost On 
    ProxyPass / http://127.0.0.1:5000/ 
    ProxyPassReverse / http://127.0.0.1:5000/ 
    ServerAdmin joaquin.duro@gmail.com 
    ServerName joakdaxpwebapp.local
    ServerAlias www.joakdaxpwebapp.local
    DocumentRoot /var/www/joakda_xp_webapp/
    <Directory /var/www/joakda_xp_webapp/> 
        Options +FollowSymLinks 
        AllowOverride All 
        Order Allow,Deny 
        Allow from All  
        Header set Access-Control-Allow-Origin "*" 
    </Directory> 
    # uncomment the following lines if you install assets as symlinks 
    # or run into problems when compiling LESS/Sass/CoffeeScript assets 
    # <Directory /var/www/project> 
    #     Options FollowSymlinks 
    # </Directory> 
    ErrorLog ${APACHE_LOG_DIR}/trdwebsvc_error.log 
    CustomLog ${APACHE_LOG_DIR}/trdwebsvc_access.log combined 
</VirtualHost> 
````
Now, enable virtual host and restart apache webserver:
````
sudo a2ensite joakdaxpwebapp.local.conf
sudo systemctl restart apache2
````
Now Apache webserve is configured to forward the requests made to http://<server_ip>:8080 to Kestrel application on http://127.0.0.1:5000, but Apache could not manage Kestrel process yet.

6. Configure Kestrel service
````
sudo nano /etc/systemd/system/kestrel-joakdaxpwebapp.service
````
Copy the following content to the previous file:
````
[Unit]
Description=JoakDa XPlane Web Application kestrel service

[Service]
WorkingDirectory=/var/www/joakda_xp_webapp
ExecStart=/usr/local/bin/dotnet /var/www/joakda_xp_webapp/JoakDAXPWebApp.dllJoakDAXPWebApp.dll
Restart=always
# Restart service after 10 seconds if the dotnet service crashes:
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=dotnet-joakdaxpwebapp
User=www-data
Environment=ASPNETCORE_ENVIRONMENT=Production

[Install]
WantedBy=multi-user.target
````
Finally, enable and start the service using the following commands:
````
sudo systemctl enable kestrel-joakdaxpwebapp.service
sudo systemctl start kestrel-joakdaxpwebapp.service
````
If we start the service, it will throw many errors related to database because it is not configured. So, now, we are going to configure MariaDB.

### Install MariaDB Server
1. Execute the following commands:
````
sudo apt update
sudo apt install software-properties-common dirmngr
sudo apt-key adv --recv-keys --keyserver keyserver.ubuntu.com 0xF1656F24C74CD1D8
sudo add-apt-repository 'deb [arch=amd64] http://mariadb.mirror.liquidtelecom.com/repo/10.4/debian stretch main'
sudo apt update
sudo apt install mariadb-server
````
2. The installation process will start. After it ends, it is needed to secure installation:
````
sudo mysql_secure_installation
````
You must answer those questions like this:
````
Enter current password for root (enter for none):
OK, successfully used password, moving on...

Setting the root password or using the unix_socket ensures that nobody
can log into the MariaDB root user without the proper authorisation.

You already have your root account protected, so you can safely answer 'n'.

Switch to unix_socket authentication [Y/n] n
 ... skipping.

You already have your root account protected, so you can safely answer 'n'.

Change the root password? [Y/n] Y
New password:
Re-enter new password:
Password updated successfully!
Reloading privilege tables..
 ... Success!


By default, a MariaDB installation has an anonymous user, allowing anyone
to log into MariaDB without having to have a user account created for
them.  This is intended only for testing, and to make the installation
go a bit smoother.  You should remove them before moving into a
production environment.

Remove anonymous users? [Y/n] y
 ... Success!

Normally, root should only be allowed to connect from 'localhost'.  This
ensures that someone cannot guess at the root password from the network.

Disallow root login remotely? [Y/n] y
 ... Success!

By default, MariaDB comes with a database named 'test' that anyone can
access.  This is also intended only for testing, and should be removed
before moving into a production environment.

Remove test database and access to it? [Y/n] y
 - Dropping test database...
 ... Success!
 - Removing privileges on test database...
 ... Success!

Reloading the privilege tables will ensure that all changes made so far
will take effect immediately.

Reload privilege tables now? [Y/n] y
 ... Success!

Cleaning up...

All done!  If you've completed all of the above steps, your MariaDB
installation should now be secure.

Thanks for using MariaDB!
````
#### Create database
1. Depending on the site configuration (connection string might be changed), it is required to configure a database with its access credentials. You can use mysql CLI or a client such as MySQL Workbench or PhpMyAdmin.
````
CREATE DATABASE app_xplane;
USE app_xplane;
CREATE USER 'u_xplane'@'localhost' IDENTIFIED VIA mysql_native_password USING '***';
GRANT USAGE ON *.* TO 'u_xplane'@'localhost' REQUIRE NONE WITH MAX_QUERIES_PER_HOUR 0 MAX_CONNECTIONS_PER_HOUR 0 MAX_UPDATES_PER_HOUR 0 MAX_USER_CONNECTIONS 0;
GRANT ALL PRIVILEGES ON `app_xplane`.* TO 'u_xplane'@'localhost'; 
FLUSH PRIVILEGES;
````

2. Although the application executes a migration to create the database schema, the database is empty, so it is needed to execute a SQL script to fill the tables:


### Client side
As Angular 10 is included inside ASP.NET Core, it is not needed to execute more installation steps. It should appear the application login on the web browser:

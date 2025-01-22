import 'package:eprodaja_mobile/providers/auth_provider.dart';
import 'package:eprodaja_mobile/providers/jedinice_mjere_provider.dart';
import 'package:eprodaja_mobile/providers/logged_product_provider.dart';
import 'package:eprodaja_mobile/providers/product_provider.dart';
import 'package:eprodaja_mobile/providers/vrste_proizvoda_provider.dart';
import 'package:eprodaja_mobile/screens/product_list_screen.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

void main() {
  runApp(MultiProvider(providers: [
    ChangeNotifierProvider<ProductProvider>(
        create: (_) => LoggedProductProvider()),
    ChangeNotifierProvider<JediniceMjereProvider>(
        create: (_) => JediniceMjereProvider()),
    ChangeNotifierProvider<VrsteProizvodaProvider>(
        create: (_) => VrsteProizvodaProvider())
  ], child: const MyApp()));
  //runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Flutter Demo',
      theme: ThemeData(
        // This is the theme of your application.
        //
        // TRY THIS: Try running your application with "flutter run". You'll see
        // the application has a purple toolbar. Then, without quitting the app,
        // try changing the seedColor in the colorScheme below to Colors.green
        // and then invoke "hot reload" (save your changes or press the "hot
        // reload" button in a Flutter-supported IDE, or press "r" if you used
        // the command line to start the app).
        //
        // Notice that the counter didn't reset back to zero; the application
        // state is not lost during the reload. To reset the state, use hot
        // restart instead.
        //
        // This works for code too, not just values: Most code changes can be
        // tested with just a hot reload.
        colorScheme: ColorScheme.fromSeed(
            seedColor: Colors.blue, primary: Colors.deepPurple),
        useMaterial3: true,
      ),
      home: LoginPage(),
    );
  }
}

class LoginPage extends StatelessWidget {
  LoginPage({super.key});

  final TextEditingController _usernameController = TextEditingController();
  final TextEditingController _passwordController = TextEditingController();
  late ProductProvider _productProvider;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      /*body: Center(
        child: Center(
            child: Container(
          constraints: const BoxConstraints(maxHeight: 400, maxWidth: 400),
          child: Card(
            child: Column(
              children: [
                /*Image.network(
                  "https://www.fit.ba/content/763cbb87-718d-4eca-a991-343858daf424",
                  height: 100,
                  width: 100,
                ),*/
                Image.asset(
                  "assets/images/logo.png",
                  height: 100,
                  width: 100,
                ),
                const SizedBox(
                  height: 10,
                ),
                TextField(
                  decoration: const InputDecoration(
                      labelText: "Username", prefixIcon: Icon(Icons.email)),
                  controller: _usernameController,
                ),
                const SizedBox(
                  height: 10,
                ),
                TextField(
                  decoration: const InputDecoration(
                      labelText: "Password", prefixIcon: Icon(Icons.password)),
                  controller: _passwordController,
                ),
                ElevatedButton(
                    onPressed: () async {
                      ProductProvider provider = ProductProvider();
                      /*var username = _usernameController.text;
                      var password = _passwordController.text;
                      print("Login attempt $username $password");*/
                      AuthProvider.username = _usernameController.text;
                      AuthProvider.password = _passwordController.text;

                      try {
                        var data = await provider.get();
                        Navigator.of(context).push(MaterialPageRoute(
                            builder: (context) => const ProductListScreen()));
                      } on Exception catch (e) {
                        showDialog(
                            context: context,
                            builder: (BuildContext context) => AlertDialog(
                                  title: const Text("Error"),
                                  content: Text(e.toString()),
                                  actions: [
                                    TextButton(
                                        onPressed: () => Navigator.pop(context),
                                        child: const Text("Ok"))
                                  ],
                                ));
                      }
                    },
                    child: const Text("Login"))
              ],
            ),
          ),
        )),
      ),*/
      body: SingleChildScrollView(
          child: Column(
        children: [
          Container(
            height: 400,
            decoration: BoxDecoration(
                image: DecorationImage(
                    image: AssetImage("assets/images/background.png"),
                    fit: BoxFit.fill)),
            child: Stack(
              children: [
                Positioned(
                  left: 30,
                  top: 0,
                  width: 120,
                  height: 160,
                  child: Container(
                    decoration: BoxDecoration(
                        image: DecorationImage(
                            image: AssetImage("assets/images/light-1.png"))),
                  ),
                ),
                Positioned(
                  right: 30,
                  top: 0,
                  width: 120,
                  height: 160,
                  child: Container(
                    decoration: BoxDecoration(
                        image: DecorationImage(
                            image: AssetImage("assets/images/clock.png"))),
                  ),
                ),
                Container(
                  child: Center(
                    child: Text(
                      "Login",
                      style: TextStyle(
                          color: Colors.white,
                          fontSize: 45,
                          fontWeight: FontWeight.bold),
                    ),
                  ),
                )
              ],
            ),
          ),
          Container(
            child: Padding(
              padding: EdgeInsets.all(20),
              child: Column(
                children: [
                  TextField(
                    decoration: InputDecoration(
                        border: InputBorder.none,
                        hintText: "Username",
                        hintStyle: TextStyle(color: Colors.grey[350])),
                  ),
                  TextField(
                    decoration: InputDecoration(
                        border: InputBorder.none,
                        hintText: "Password",
                        hintStyle: TextStyle(color: Colors.grey[350])),
                  )
                ],
              ),
            ),
          ),
          Padding(
            padding: const EdgeInsets.all(20.0),
            child: Container(
              height: 50,
              decoration: BoxDecoration(
                  borderRadius: BorderRadius.circular(10),
                  gradient: LinearGradient(colors: [
                    Color.fromRGBO(143, 148, 251, 1),
                    Color.fromRGBO(143, 148, 251, 6)
                  ])),
              child: InkWell(
                onTap: () {},
                child: Center(
                    child: Text("Login",
                        style: TextStyle(
                            color: Colors.white,
                            fontSize: 20,
                            fontWeight: FontWeight.bold))),
              ),
            ),
          )
        ],
      )),
    );
  }
}

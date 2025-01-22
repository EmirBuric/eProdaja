import 'package:eprodaja_mobile/screens/product_details_screen.dart';
import 'package:eprodaja_mobile/screens/product_list_screen.dart';
import 'package:eprodaja_mobile/screens/user_list_screen.dart';
import 'package:flutter/material.dart';

class MasterScreenWidget extends StatefulWidget {
  Widget? child;
  String? title;
  Widget? title_widget;
  MasterScreenWidget({this.child, this.title, this.title_widget, super.key});

  @override
  State<MasterScreenWidget> createState() => _MasterScreenWidgetState();
}

class _MasterScreenWidgetState extends State<MasterScreenWidget> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
        appBar: AppBar(title: widget.title_widget ?? Text(widget.title ?? "")),
        drawer: Drawer(
          child: ListView(
            children: [
              ListTile(
                title: const Text('Back'),
                onTap: () {
                  Navigator.of(context).pop();
                  Navigator.of(context).pop();
                },
              ),
              ListTile(
                title: const Text('Proizovdi'),
                onTap: () {
                  Navigator.of(context).push(MaterialPageRoute(
                      builder: (context) => const ProductListScreen()));
                },
              ),
              ListTile(
                title: const Text('Detalji'),
                onTap: () {
                  Navigator.of(context).push(MaterialPageRoute(
                      builder: (context) => ProductDetailScreen()));
                },
              ),
              ListTile(
                title: const Text("Korisnici"),
                onTap: () {
                  Navigator.of(context).pushReplacement(MaterialPageRoute(
                      builder: (context) => const UserListScreen()));
                },
              )
            ],
          ),
        ),
        body: widget.child!);
  }
}

import 'package:eprodaja_admin/screens/product_details_screen.dart';
import 'package:eprodaja_admin/screens/product_list_screen.dart';
import 'package:eprodaja_admin/screens/user_list_screen.dart';
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
                title: Text('Back'),
                onTap: () {
                  Navigator.of(context).pop();
                  Navigator.of(context).pop();
                },
              ),
              ListTile(
                title: Text('Proizovdi'),
                onTap: () {
                  Navigator.of(context).push(MaterialPageRoute(
                      builder: (context) => const ProductListScreen()));
                },
              ),
              ListTile(
                title: Text('Detalji'),
                onTap: () {
                  Navigator.of(context).push(MaterialPageRoute(
                      builder: (context) => const ProductDetailScreen()));
                },
              ),
              ListTile(
                title: Text("Korisnici"),
                onTap: () {
                  Navigator.of(context).pushReplacement(MaterialPageRoute(
                      builder: (context) => UserListScreen()));
                },
              )
            ],
          ),
        ),
        body: widget.child!);
  }
}

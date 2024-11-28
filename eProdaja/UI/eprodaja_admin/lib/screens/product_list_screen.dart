import 'package:eprodaja_admin/layouts/master_screen.dart';
import 'package:eprodaja_admin/providers/product_provider.dart';
import 'package:eprodaja_admin/screens/product_details_screen.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

class ProductListScreen extends StatefulWidget {
  const ProductListScreen({super.key});

  @override
  State<ProductListScreen> createState() => _ProductListScreenState();
}

class _ProductListScreenState extends State<ProductListScreen> {
  late ProductProvider _productProvider;

  @override
  void didChangeDependencies() {
    // TODO: implement didChangeDependencies
    super.didChangeDependencies();
    _productProvider = context.read<ProductProvider>();
  }

  @override
  Widget build(BuildContext context) {
    return MasterScreenWidget(
        title_widget: Text("Prodict list"),
        child: Container(
            child: Column(children: [
          const Text("Test"),
          ElevatedButton(
              onPressed: () async {
                print("Login attempt");
                //Navigator.of(context).pop();
                /*Navigator.of(context).push(MaterialPageRoute(
                    builder: (context) => const ProductDetailScreen()));*/
                var data = await _productProvider.get();
                print("data: ${data['resultsList'][0]['naziv']}");
              },
              child: const Text("Login"))
        ])));
  }
}

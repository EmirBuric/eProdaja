import 'package:eprodaja_admin/layouts/master_screen.dart';
import 'package:eprodaja_admin/models/proizvod.dart';
import 'package:eprodaja_admin/models/search_result.dart';
import 'package:eprodaja_admin/providers/product_provider.dart';
import 'package:eprodaja_admin/providers/utils.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:provider/provider.dart';

class ProductListScreen extends StatefulWidget {
  const ProductListScreen({super.key});

  @override
  State<ProductListScreen> createState() => _ProductListScreenState();
}

class _ProductListScreenState extends State<ProductListScreen> {
  late ProductProvider provider;

  @override
  void didChangeDependencies() {
    super.didChangeDependencies();

    provider = context.read<ProductProvider>();
  }

  SearchResult<Proizvod>? data = null;

  @override
  Widget build(BuildContext context) {
    return MasterScreenWidget(
        title_widget: Text("Lista proizvoda"),
        child: Container(
            child: Column(children: [
          const Text("Test"),
          ElevatedButton(
              onPressed: () async {
                print("Login attempt");
                //Navigator.of(context).pop();
                /*Navigator.of(context).push(MaterialPageRoute(
                    builder: (context) => const ProductDetailScreen()));*/
                data = await provider.get();
                //print(data[0].naziv);
              },
              child: const Text("Login")),
          _buildSearch(),
          _buildResultView()
        ])));
  }

  TextEditingController _ftsEditingController = new TextEditingController();
  TextEditingController _sifraEditingController = new TextEditingController();
  Widget _buildSearch() {
    return Padding(
      padding: const EdgeInsets.all(8.0),
      child: Row(
        children: [
          Expanded(
              child: TextField(
                  controller: _ftsEditingController,
                  decoration: InputDecoration(labelText: "Naziv ili sifra"))),
          SizedBox(
            width: 8,
          ),
          Expanded(
              child: TextField(
                  controller: _sifraEditingController,
                  decoration: InputDecoration(labelText: "Sifra"))),
          ElevatedButton(
              onPressed: () async {
                var filter = {
                  "fts": _ftsEditingController.text,
                  "sifra": _sifraEditingController.text
                };
                data = await provider.get(filter: filter);
                setState(() {});
                //print(data[0].proizvodId);
              },
              child: Text("Pretraga")),
          SizedBox(
            width: 8,
          ),
          ElevatedButton(onPressed: () async {}, child: Text("Dodaj")),
        ],
      ),
    );
  }

  Widget _buildResultView() {
    return Expanded(
        child: SingleChildScrollView(
      child: DataTable(
        columns: [
          DataColumn(label: Text("ID"), numeric: true),
          DataColumn(label: Text("Naziv")),
          DataColumn(label: Text("Šifra")),
          DataColumn(label: Text("Cijena")),
          DataColumn(label: Text("Slika")),
        ],
        /*rows: [
          DataRow(cells: [
            DataCell(Container(child: Text("1"), width: 150)),
            DataCell(Container(child: Text("Laptop"), width: double.infinity))
          ]),
          DataRow(cells: [
            DataCell(Container(
              child: Text("2"),
              width: 150,
            )),
            DataCell(Container(child: Text("Monitor"), width: double.infinity))
          ])
        ],*/
        rows: data?.result
                .map((e) => DataRow(cells: [
                      DataCell(Text(e.proizvodId.toString())),
                      DataCell(Text(e.naziv ?? "")),
                      DataCell(Text(e.sifra ?? "")),
                      DataCell(Text(formatNumber(e.cijena))),
                      DataCell(e.slika != null
                          ? Container(
                              width: 100,
                              height: 100,
                              child: imageFromString(e.slika!),
                            )
                          : Text("")),
                    ]))
                .toList()
                .cast<DataRow>() ??
            [],
      ),
    ));
  }
}

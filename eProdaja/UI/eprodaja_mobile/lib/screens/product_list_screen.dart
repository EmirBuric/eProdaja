import 'package:eprodaja_mobile/layouts/master_screen.dart';
import 'package:eprodaja_mobile/models/proizvod.dart';
import 'package:eprodaja_mobile/models/search_result.dart';
import 'package:eprodaja_mobile/providers/product_provider.dart';
import 'package:eprodaja_mobile/providers/utils.dart';
import 'package:eprodaja_mobile/screens/product_details_screen.dart';
import 'package:flutter/material.dart';
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

  SearchResult<Proizvod>? data;

  @override
  Widget build(BuildContext context) {
    return MasterScreenWidget(
        title_widget: const Text("Lista proizvoda"),
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

  final TextEditingController _ftsEditingController = TextEditingController();
  final TextEditingController _sifraEditingController = TextEditingController();
  Widget _buildSearch() {
    return Padding(
      padding: const EdgeInsets.all(8.0),
      child: Row(
        children: [
          Expanded(
              child: TextField(
                  controller: _ftsEditingController,
                  decoration: const InputDecoration(labelText: "Naziv ili sifra"))),
          const SizedBox(
            width: 8,
          ),
          Expanded(
              child: TextField(
                  controller: _sifraEditingController,
                  decoration: const InputDecoration(labelText: "Sifra"))),
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
              child: const Text("Pretraga")),
          const SizedBox(
            width: 8,
          ),
          ElevatedButton(
              onPressed: () async {
                Navigator.of(context).push(MaterialPageRoute(
                    builder: (context) => ProductDetailScreen()));
              },
              child: const Text("Dodaj")),
        ],
      ),
    );
  }

  Widget _buildResultView() {
    return Expanded(
        child: SizedBox(
            width: double.infinity,
            child: SingleChildScrollView(
              child: DataTable(
                columns: const [
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
                        .map((e) => DataRow(
                                onSelectChanged: (selected) => {
                                      if (selected == true)
                                        {
                                          Navigator.of(context).push(
                                              MaterialPageRoute(
                                                  builder: (context) =>
                                                      ProductDetailScreen(
                                                          product: e)))
                                        }
                                    },
                                cells: [
                                  DataCell(Text(e.proizvodId.toString())),
                                  DataCell(Text(e.naziv ?? "")),
                                  DataCell(Text(e.sifra ?? "")),
                                  DataCell(Text(formatNumber(e.cijena))),
                                  DataCell(e.slika != null
                                      ? SizedBox(
                                          width: 100,
                                          height: 100,
                                          child: imageFromString(e.slika!),
                                        )
                                      : const Text("")),
                                ]))
                        .toList()
                        .cast<DataRow>() ??
                    [],
              ),
            )));
  }
}

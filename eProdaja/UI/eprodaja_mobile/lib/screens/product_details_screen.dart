import 'dart:convert';
import 'dart:io';

import 'package:eprodaja_mobile/layouts/master_screen.dart';
import 'package:eprodaja_mobile/models/jedinice_mjere.dart';
import 'package:eprodaja_mobile/models/proizvod.dart';
import 'package:eprodaja_mobile/models/search_result.dart';
import 'package:eprodaja_mobile/models/vrste_proizvoda.dart';
import 'package:eprodaja_mobile/providers/jedinice_mjere_provider.dart';
import 'package:eprodaja_mobile/providers/product_provider.dart';
import 'package:eprodaja_mobile/providers/vrste_proizvoda_provider.dart';
import 'package:file_picker/file_picker.dart';
import 'package:flutter/material.dart';
import 'package:flutter_form_builder/flutter_form_builder.dart';
import 'package:provider/provider.dart';

class ProductDetailScreen extends StatefulWidget {
  Proizvod? product;
  ProductDetailScreen({super.key, this.product});

  @override
  State<ProductDetailScreen> createState() => _ProductDetailScreenState();
}

class _ProductDetailScreenState extends State<ProductDetailScreen> {
  final _formKey = GlobalKey<FormBuilderState>();
  Map<String, dynamic> _initialValue = {};

  late ProductProvider productProvider;
  late JediniceMjereProvider jediniceMjereProvider;
  late VrsteProizvodaProvider vrsteProizvodaProvider;
  SearchResult<VrsteProizvoda>? vrstaProizvodaResult;
  SearchResult<JediniceMjere>? jediniceMjereResult;
  bool isLoading = true;

  @override
  void didChangeDependencies() {
    super.didChangeDependencies();
  }

  @override
  void initState() {
    productProvider = context.read<ProductProvider>();
    jediniceMjereProvider = context.read<JediniceMjereProvider>();
    vrsteProizvodaProvider = context.read<VrsteProizvodaProvider>();
    // TODO: implement initState
    super.initState();

    _initialValue = {
      'sifra': widget.product?.sifra,
      'naziv': widget.product?.naziv,
      'cijena': widget.product?.cijena.toString(),
      'vrstaId': widget.product?.vrstaId.toString(),
      'jedinicaMjereId': widget.product?.jedinicaMjereId.toString(),
    };

    initForm();
  }

  Future initForm() async {
    vrstaProizvodaResult = await vrsteProizvodaProvider.get();
    jediniceMjereResult = await jediniceMjereProvider.get();
    print("retrived jediniceMjere:${jediniceMjereResult?.result.length}");
    print("retrived vrsteProizvoda:${vrstaProizvodaResult?.result.length}");
    setState(() {
      isLoading = false;
    });
  }

  @override
  Widget build(BuildContext context) {
    return MasterScreenWidget(
      title: "Product Details",
      child: Column(
        children: [isLoading ? Container() : _buildForm(), _saveRow()],
      ),
    );
  }

  Widget _buildForm() {
    return FormBuilder(
      key: _formKey,
      initialValue: _initialValue,
      child: Padding(
        padding: const EdgeInsets.all(8.0),
        child: Column(
          children: [
            Row(
              children: [
                Expanded(
                    child: FormBuilderTextField(
                  decoration: const InputDecoration(labelText: "Šifra"),
                  name: "sifra",
                )),
                const SizedBox(width: 10),
                Expanded(
                    child: FormBuilderTextField(
                  decoration: const InputDecoration(labelText: "Naziv"),
                  name: "naziv",
                ))
              ],
            ),
            Row(
              children: [
                Expanded(
                    child: FormBuilderDropdown(
                  name: "vrstaId",
                  decoration: const InputDecoration(labelText: "Vrsta Proizvoda"),
                  items: vrstaProizvodaResult?.result
                          .map((item) => DropdownMenuItem(
                              value: item.vrstaId.toString(),
                              child: Text(item.naziv ?? "")))
                          .toList() ??
                      [],
                )),
                const SizedBox(width: 10),
                Expanded(
                    child: FormBuilderDropdown(
                  name: "jedinicaMjereId",
                  decoration: const InputDecoration(labelText: "Jedinica Mjere"),
                  items: jediniceMjereResult?.result
                          .map((item) => DropdownMenuItem(
                              value: item.jedinicaMjereId.toString(),
                              child: Text(item.naziv ?? "")))
                          .toList() ??
                      [],
                )),
                const SizedBox(width: 10),
                Expanded(
                    child: FormBuilderTextField(
                  decoration: const InputDecoration(labelText: "Cijena"),
                  name: "cijena",
                ))
              ],
            ),
            Row(
              children: [
                Expanded(
                    child: FormBuilderField(
                        name: "imageId",
                        builder: (field) {
                          return InputDecorator(
                              decoration:
                                  const InputDecoration(labelText: "Odaberite sliku"),
                              child: ListTile(
                                leading: const Icon(Icons.photo),
                                title: const Text("Select image"),
                                trailing: const Icon(Icons.file_upload),
                                onTap: getImage,
                              ));
                        }))
              ],
            )
          ],
        ),
      ),
    );
  }

  Widget _saveRow() {
    return Padding(
      padding: const EdgeInsets.all(8.0),
      child: Row(
        mainAxisAlignment: MainAxisAlignment.end,
        children: [
          ElevatedButton(
              onPressed: () {
                _formKey.currentState?.saveAndValidate();
                debugPrint(_formKey.currentState?.value.toString());
                var request = Map.from(_formKey.currentState!.value);

                request["slika"] = _base64image;
                if (widget.product == null) {
                  productProvider.insert(request);
                } else {
                  productProvider.update(widget.product!.proizvodId!, request);
                }
              },
              child: const Text("Sacuvaj"))
        ],
      ),
    );
  }

  File? _image;
  String? _base64image;
  void getImage() async {
    var result = await FilePicker.platform.pickFiles(type: FileType.image);

    if (result != null && result.files.single.path != null) {
      _image = File(result.files.single.path!);
      _base64image = base64Encode(_image!.readAsBytesSync());
    }
  }
}

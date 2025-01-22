import 'dart:convert';
import 'dart:io';

import 'package:eprodaja_admin/layouts/master_screen.dart';
import 'package:eprodaja_admin/models/jedinice_mjere.dart';
import 'package:eprodaja_admin/models/proizvod.dart';
import 'package:eprodaja_admin/models/search_result.dart';
import 'package:eprodaja_admin/models/vrste_proizvoda.dart';
import 'package:eprodaja_admin/providers/jedinice_mjere_provider.dart';
import 'package:eprodaja_admin/providers/product_provider.dart';
import 'package:eprodaja_admin/providers/vrste_proizvoda_provider.dart';
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
  SearchResult<VrsteProizvoda>? vrstaProizvodaResult = null;
  SearchResult<JediniceMjere>? jediniceMjereResult = null;
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
      child: Column(
        children: [isLoading ? Container() : _buildForm(), _saveRow()],
      ),
      title: "Product Details",
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
                  decoration: InputDecoration(labelText: "Šifra"),
                  name: "sifra",
                )),
                SizedBox(width: 10),
                Expanded(
                    child: FormBuilderTextField(
                  decoration: InputDecoration(labelText: "Naziv"),
                  name: "naziv",
                ))
              ],
            ),
            Row(
              children: [
                Expanded(
                    child: FormBuilderDropdown(
                  name: "vrstaId",
                  decoration: InputDecoration(labelText: "Vrsta Proizvoda"),
                  items: vrstaProizvodaResult?.result
                          .map((item) => DropdownMenuItem(
                              value: item.vrstaId.toString(),
                              child: Text(item.naziv ?? "")))
                          .toList() ??
                      [],
                )),
                SizedBox(width: 10),
                Expanded(
                    child: FormBuilderDropdown(
                  name: "jedinicaMjereId",
                  decoration: InputDecoration(labelText: "Jedinica Mjere"),
                  items: jediniceMjereResult?.result
                          .map((item) => DropdownMenuItem(
                              value: item.jedinicaMjereId.toString(),
                              child: Text(item.naziv ?? "")))
                          .toList() ??
                      [],
                )),
                SizedBox(width: 10),
                Expanded(
                    child: FormBuilderTextField(
                  decoration: InputDecoration(labelText: "Cijena"),
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
                                  InputDecoration(labelText: "Odaberite sliku"),
                              child: ListTile(
                                leading: Icon(Icons.photo),
                                title: Text("Select image"),
                                trailing: Icon(Icons.file_upload),
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
                var request = new Map.from(_formKey.currentState!.value);

                request["slika"] = _base64image;
                if (widget.product == null) {
                  productProvider.insert(request);
                } else {
                  productProvider.update(widget.product!.proizvodId!, request);
                }
              },
              child: Text("Sacuvaj"))
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

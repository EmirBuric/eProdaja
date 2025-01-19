import 'dart:convert';

import 'package:eprodaja_admin/models/proizvod.dart';
import 'package:eprodaja_admin/models/search_result.dart';
import 'package:eprodaja_admin/providers/auth_provider.dart';
import 'package:eprodaja_admin/providers/base_provider.dart';
import 'package:http/http.dart' as http;
import 'package:http/http.dart';

class ProductProvider extends BaseProvider<Proizvod> {
  ProductProvider() : super("Proizvodi");

  @override
  Proizvod fromJson(data) {
    // TODO: implement fromJson
    return Proizvod.fromJson(data);
  }
}

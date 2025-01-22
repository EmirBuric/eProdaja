import 'dart:convert';

import 'package:eprodaja_admin/models/jedinice_mjere.dart';
import 'package:eprodaja_admin/models/proizvod.dart';
import 'package:eprodaja_admin/providers/base_provider.dart';
import 'package:http/http.dart' as http;
import 'package:http/http.dart';

class JediniceMjereProvider extends BaseProvider<JediniceMjere> {
  JediniceMjereProvider() : super("JediniceMjere");

  @override
  JediniceMjere fromJson(data) {
    // TODO: implement fromJson
    return JediniceMjere.fromJson(data);
  }
}

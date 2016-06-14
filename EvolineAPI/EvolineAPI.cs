using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;


namespace Evoline.API {
    public class EvolineAPI {


        private String accessToken;
        private bool debug = false;

        public EvolineAPI(String accessToken) {
            this.accessToken = accessToken;
        }

        public void debugOn() {
            this.debug = true;
        }

        public void debugOff() {
            this.debug = false;
        }



        private dynamic sendRequest(String path, String method, String body = null) {

            if (this.debug) {
                Console.WriteLine("Evoline Request: " + path + " Method: " + method + " Body: " + body);
            }

            WebRequest request = WebRequest.Create("https://api.evoline.com.br" + path);
            request.Method = method;
            request.Headers.Add("Access-Token",this.accessToken);
            if (body != null) {
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(body);

                request.ContentLength = bytes.Length;
                System.IO.Stream reqStream = request.GetRequestStream();
                reqStream.Write(bytes, 0, bytes.Length);
                try {
                    reqStream.Close();
                }catch(Exception e) {
                    Console.WriteLine(e);
                }
            }

            HttpWebResponse response = null;
            try {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex) {
                if (ex.Status == WebExceptionStatus.ProtocolError && ex.Response != null) {
                    response = (HttpWebResponse)ex.Response;

                }
            }



            System.IO.Stream resStream = response.GetResponseStream();
            System.IO.StreamReader reader = new System.IO.StreamReader(resStream);

            String responseFromServer = reader.ReadToEnd();

            reader.Close();
            resStream.Close();
            response.Close();


            if (this.debug) {
                Console.WriteLine("Evoline Response: " + responseFromServer);
            }

            return JsonConvert.DeserializeObject(responseFromServer);

        }




        /* CONTA */

        public dynamic consultaSaldo() {
            return this.sendRequest("/saldo", "GET");
        }

        public dynamic minhaConta() {
            return this.sendRequest("/conta", "GET");
        }

        public dynamic atualizaDadosConta(String nome, String login, String senha, String cpf_cnpj, String telefone) {
            Dictionary<string, dynamic> body = new Dictionary<string, dynamic>();

            if (nome != null) {
                body.Add("nome", nome);
            }
            if (login != null) {
                body.Add("login", login);
            }
            if (senha != null) {
                body.Add("senha", senha);
            }
            if (cpf_cnpj != null) {
                body.Add("cpf_cnpj", cpf_cnpj);
            }
            if (telefone != null) {
                body.Add("telefone", telefone);
            }


            return this.sendRequest("/conta", "PUT", JsonConvert.SerializeObject(body));
        }




        /* CHAMADA */

        public dynamic enviaChamada(String origem, String destino, bool gravar_audio = false, String bina_origem = null, String bina_destino = null) {
            Dictionary<string, dynamic> body = new Dictionary<string, dynamic>();
            body.Add("numero_origem", origem);
            body.Add("numero_destino", destino);
            body.Add("gravar_audio", gravar_audio);
            if (bina_origem != null) {
                body.Add("bina_origem", bina_origem);
            }
            if (bina_destino != null) {
                body.Add("bina_destino", bina_destino);
            }

            return this.sendRequest("/chamada", "POST", JsonConvert.SerializeObject(body));
        }

        public dynamic cancelaChamada(int chamadaId) {
            return this.sendRequest("/chamada/" + chamadaId, "DELETE");
        }

        public dynamic statusChamada(int chamadaId) {
            return this.sendRequest("/chamada/" + chamadaId, "GET");
        }

        public dynamic relatorioChamadas(String dataInicio, String dataFim) {
            return this.sendRequest("/chamada/relatorio?data_inicio=" + dataInicio + "&data_fim=" + dataFim, "GET");
        }



        /* SMS */

        public dynamic enviaSMS(String numero_destino, String mensagem) {
            Dictionary<string, dynamic> body = new Dictionary<string, dynamic>();

            body.Add("numero_destino", numero_destino);
            body.Add("mensagem", mensagem);


            return this.sendRequest("/sms", "POST", JsonConvert.SerializeObject(body);
        }

        public dynamic statusSMS(int smsId) {
            return this.sendRequest("/sms/" + smsId, "GET");
        }

        public dynamic relatorioSMS(String dataInicio, String dataFim) {
            return this.sendRequest("/sms/relatorio?data_inicio=" + dataInicio + "&data_fim=" + dataFim, "GET");
        }



        /* TTS */

        public dynamic enviaTTS(String numero_destino, String mensagem, int velocidade, bool resposta_usuario = false) {
            Dictionary<string, dynamic> body = new Dictionary<string, dynamic>();

            body.Add("numero_destino", numero_destino);
            body.Add("mensagem", mensagem);
            body.Add("velocidade", velocidade);
            body.Add("resposta_usuario",  resposta_usuario);


            return this.sendRequest("/tts", "POST", JsonConvert.SerializeObject(body));
        }

        public dynamic statusTTS(int ttsIs) {
            return this.sendRequest("/tts/" + ttsIs, "GET");
        }

        public dynamic relatorioTTS(String dataInicio, String dataFim) {
            return this.sendRequest("/tts/relatorio?data_inicio=" + dataInicio + "&data_fim=" + dataFim, "GET");
        }



        /* Audio */
        
        public dynamic enviaAudio(String numero_destino, String url_audio, bool resposta_usuario = false) {
            Dictionary<string, dynamic> body = new Dictionary<string, dynamic>();

            body.Add("numero_destino", numero_destino);
            body.Add("url_audio", url_audio);
            body.Add("resposta_usuario", resposta_usuario);

            return this.sendRequest("/audio", "POST", JsonConvert.SerializeObject(body));
        }

        public dynamic statusAudio(int audioId) {
            return this.sendRequest("/audio/" + audioId, "GET");
        }

        public dynamic relatorioAudio(String dataInicio, String dataFim) {
            return this.sendRequest("/audio/relatorio?data_inicio=" + dataInicio + "&data_fim=" + dataFim, "GET");
        }



    }
}

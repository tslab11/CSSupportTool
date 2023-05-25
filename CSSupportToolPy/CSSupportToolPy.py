
from flask import Flask, jsonify, request

class tModel(object):
    """ XXX """
    
    def __init__(self) -> None:
        """ XXX """
        self.sample1
        self.sample2
        self.sample3
        self.sample4
        return


app = Flask(__name__)

# GETリクエストに対するルートエンドポイント
@app.route('/api/data', methods=['GET'])
def get_data():
    data = {'message': 'Hello, world!'}
    return jsonify(data)

# POSTリクエストに対するルートエンドポイント
@app.route('/api/data', methods=['POST'])
def post_data():
    request_data = request.get_json()
    print(request_data)
    # リクエストからデータを取得して処理する
    # ...

    #response_data = {'message': 'Data received successfully'}
    response_data = request_data;
    return jsonify(response_data)

if __name__ == '__main__':
    app.run(port = 5000)